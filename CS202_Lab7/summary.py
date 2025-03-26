#!/usr/bin/env python3

import pandas as pd
import numpy as np

# List the CSV files you want to analyze
csv_files = [
    "qlib_bandit_analysis.csv",
    "requests_bandit_analysis.csv",
    "scapy_bandit_analysis.csv"
]

summaries = []

for csv_file in csv_files:
    # Read the CSV into a DataFrame
    df = pd.read_csv(csv_file)
    
    # Calculate average confidence values
    avg_high_conf = df["high_confidence"].mean()
    avg_med_conf = df["medium_confidence"].mean()
    avg_low_conf = df["low_confidence"].mean()
    
    # Calculate average severity values
    avg_high_sev = df["high_severity"].mean()
    avg_med_sev = df["medium_severity"].mean()
    avg_low_sev = df["low_severity"].mean()
    
    # Gather all CWE identifiers (if any)
    all_cwes = set()
    for cwe_str in df["unique_cwes"]:
        if pd.isna(cwe_str) or not cwe_str.strip():
            continue
        # Some rows may have multiple CWEs separated by commas
        for cwe in cwe_str.split(","):
            cwe = cwe.strip()
            if cwe:
                all_cwes.add(cwe)
    
    # Prepare a summary dictionary for this repository
    repo_name = csv_file.replace("_bandit_analysis.csv", "")
    summaries.append({
        "repository": repo_name,
        "avg_high_confidence": avg_high_conf,
        "avg_medium_confidence": avg_med_conf,
        "avg_low_confidence": avg_low_conf,
        "avg_high_severity": avg_high_sev,
        "avg_medium_severity": avg_med_sev,
        "avg_low_severity": avg_low_sev,
        "unique_cwes_count": len(all_cwes),
        "unique_cwes_list": sorted(all_cwes)
    })

# Print out a simple table in the console
print("Summary of Bandit Analysis:\n")
print(f"{'Repository':<15} | "
      f"{'Avg High Conf':>12} | "
      f"{'Avg Med Conf':>12} | "
      f"{'Avg Low Conf':>12} | "
      f"{'Avg High Sev':>12} | "
      f"{'Avg Med Sev':>12} | "
      f"{'Avg Low Sev':>12} | "
      f"{'CWE Count':>9}")
print("-" * 107)

for summary in summaries:
    print(f"{summary['repository']:<15} | "
          f"{summary['avg_high_confidence']:>12.2f} | "
          f"{summary['avg_medium_confidence']:>12.2f} | "
          f"{summary['avg_low_confidence']:>12.2f} | "
          f"{summary['avg_high_severity']:>12.2f} | "
          f"{summary['avg_medium_severity']:>12.2f} | "
          f"{summary['avg_low_severity']:>12.2f} | "
          f"{summary['unique_cwes_count']:>9}")

# Optionally, print full details including the sorted CWE list
print("\nDetailed CWE Lists:")
for summary in summaries:
    print(f"{summary['repository']}: {summary['unique_cwes_list']}")
