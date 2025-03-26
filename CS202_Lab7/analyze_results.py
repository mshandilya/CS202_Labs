#!/usr/bin/env python3
import pandas as pd
import matplotlib.pyplot as plt
import glob

# Combine all CSV results into one DataFrame
csv_files = glob.glob("*_bandit_analysis.csv")
df_list = [pd.read_csv(file) for file in csv_files]
data = pd.concat(df_list, ignore_index=True)

# Convert commit_date to datetime
data["commit_date"] = pd.to_datetime(data["commit_date"])

# --- Research Question 1 ---
# RQ1 (high severity): When are high severity vulnerabilities introduced and fixed?
# For demonstration, we plot the count of high severity issues over time.
plt.figure(figsize=(10, 6))
sorted_data = data.sort_values(by="commit_date")
plt.plot(sorted_data["commit_date"], sorted_data["high_severity"], marker="o", linestyle="-")
plt.xlabel("Commit Date")
plt.ylabel("High Severity Issues")
plt.title("High Severity Vulnerabilities Over Time")
plt.xticks(rotation=45)
plt.tight_layout()
plt.savefig("RQ1_high_severity_timeline.png")
plt.show()

# --- Research Question 2 ---
# RQ2 (different severity): Compare patterns of introduction/elimination for different severity levels.
plt.figure(figsize=(10, 6))
plt.plot(sorted_data["commit_date"], sorted_data["high_severity"], marker="o", label="High Severity")
plt.plot(sorted_data["commit_date"], sorted_data["medium_severity"], marker="o", label="Medium Severity")
plt.plot(sorted_data["commit_date"], sorted_data["low_severity"], marker="o", label="Low Severity")
plt.xlabel("Commit Date")
plt.ylabel("Number of Issues")
plt.title("Vulnerabilities by Severity Over Time")
plt.legend()
plt.xticks(rotation=45)
plt.tight_layout()
plt.savefig("RQ2_severity_comparison.png")
plt.show()

# --- Research Question 3 ---
# RQ3 (CWE coverage): Which CWEs are most frequent?
# Explode the CWE column (which is comma-separated) into multiple rows.
# def explode_cwes(cwe_str):
#     if pd.isna(cwe_str) or cwe_str.strip() == "":
#         return []
#     return [cwe.strip() for cwe in cwe_str.split(",")]

# cwe_series = data["unique_cwes"].apply(explode_cwes)
# # Flatten the list of CWEs
# all_cwes = [cwe for sublist in cwe_series for cwe in sublist]
# cwe_counts = pd.Series(all_cwes).value_counts()

# # Bar chart of CWE frequencies
# plt.figure(figsize=(10, 6))
# cwe_counts.plot(kind="bar")
# plt.xlabel("CWE")
# plt.ylabel("Frequency")
# plt.title("Frequency of CWEs Across Repositories")
# plt.tight_layout()
# plt.savefig("RQ3_cwe_frequency.png")
# plt.show()

# Optionally, save the aggregated results
data.to_csv("aggregated_bandit_analysis.csv", index=False)
