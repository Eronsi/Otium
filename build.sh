#!/bin/bash

echo Fetch...
git fetch
git status
echo Pull...
git pull
echo Start building...
cd /root/work/Otium/Otium/Otium && dotnet publish -c Release -o ../Release
echo Build end.
echo Restarting service...
systemctl restart otiumbuild.service
echo Service restared.
echo Done!