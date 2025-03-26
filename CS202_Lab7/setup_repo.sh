#!/bin/bash

# Usage: ./setup_repo.sh <repo_url> <repo_directory>

if [ "$#" -ne 2 ]; then
    echo "Usage: $0 <repo_url> <repo_directory>"
    exit 1
fi

REPO_URL=$1
REPO_DIR=$2

# Clone the repository
git clone $REPO_URL $REPO_DIR
cd $REPO_DIR || exit

# Create and activate a virtual environment
python3 -m venv venv
source venv/bin/activate

# Install bandit (and other dependencies if required)
pip install --upgrade pip
pip install bandit

# Optionally install project dependencies if needed
if [ -f requirements.txt ]; then
    pip install -r requirements.txt
fi

echo "Repository setup complete in $REPO_DIR."
