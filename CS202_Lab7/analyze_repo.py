#!/usr/bin/env python3
import os
import json
import csv
import subprocess
from datetime import datetime

# Path to the repository (passed as an argument)
import sys
if len(sys.argv) != 2:
    print("Usage: python analyze_repo.py <repository_path>")
    sys.exit(1)

repo_path = sys.argv[1]
os.chdir(repo_path)

# Get the last 100 non-merge commits (hash and commit date)
commit_log = subprocess.check_output(
    ['git', 'log', '--no-merges', '-n', '100', '--pretty=format:%H;%ci'],
    universal_newlines=True
).strip().splitlines()

commits = []
for line in commit_log:
    try:
        commit_hash, commit_date = line.split(";")
        # Parse commit_date to a standard format (optional)
        commit_date = datetime.strptime(commit_date.strip(), "%Y-%m-%d %H:%M:%S %z")
        commits.append((commit_hash, commit_date))
    except ValueError:
        continue

# Prepare CSV output file
csv_filename = os.path.join("..", f"{os.path.basename(repo_path)}_bandit_analysis.csv")
with open(csv_filename, mode="w", newline="") as csvfile:
    csv_writer = csv.writer(csvfile)
    # CSV header: commit hash, commit date, counts for confidence and severity, unique CWEs (as a comma-separated list)
    csv_writer.writerow([
        "commit_hash", "commit_date",
        "high_confidence", "medium_confidence", "low_confidence",
        "high_severity", "medium_severity", "low_severity",
        "unique_cwes"
    ])

    # Loop over each commit
    for commit_hash, commit_date in commits:
        # Checkout the commit
        subprocess.run(["git", "checkout", commit_hash], stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        
        # Run bandit on the repository recursively, outputting JSON
        bandit_output = "bandit_output.json"
        subprocess.run(["bandit", "-r", ".", "-f", "json", "-o", bandit_output],
                       stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        
        # Initialize counters
        high_conf = medium_conf = low_conf = 0
        high_sev = medium_sev = low_sev = 0
        cwes = set()

        # Load and parse bandit's JSON output
        try:
            with open(bandit_output, "r") as f:
                data = json.load(f)
            for issue in data.get("results", []):
                # Count based on confidence
                conf = issue.get("issue_confidence", "").upper()
                if conf == "HIGH":
                    high_conf += 1
                elif conf == "MEDIUM":
                    medium_conf += 1
                elif conf == "LOW":
                    low_conf += 1

                # Count based on severity
                sev = issue.get("issue_severity", "").upper()
                if sev == "HIGH":
                    high_sev += 1
                elif sev == "MEDIUM":
                    medium_sev += 1
                elif sev == "LOW":
                    low_sev += 1

                # Collect CWE information if available
                cwe = issue.get("cwe")
                if cwe and cwe != "N/A":
                    cwes.add(cwe)
        except Exception as e:
            print(f"Error processing bandit output for commit {commit_hash}: {e}")

        # Write the results for this commit to CSV
        csv_writer.writerow([
            commit_hash,
            commit_date.strftime("%Y-%m-%d %H:%M:%S"),
            high_conf, medium_conf, low_conf,
            high_sev, medium_sev, low_sev,
            ",".join(sorted(cwes))
        ])

        # Optionally, remove the temporary bandit output file
        if os.path.exists(bandit_output):
            os.remove(bandit_output)

print(f"Analysis complete. Results saved in {csv_filename}")

# Return to the main branch after analysis
subprocess.run(["git", "checkout", "main"], stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
