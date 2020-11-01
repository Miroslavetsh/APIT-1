#!/bin/bash

echo " ===== START ====="

# Read properties from terminal
migration_name=$1
opt=$2

# Apply migration and save result
dotnet ef migrations add $migration_name $opt
dotnet ef database update

echo " ===== FINISH ====="
exit 0

