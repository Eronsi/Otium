#!/bin/bash

cd /root/work/Otium
echo Fetch...
git fetch
git status
echo Pull...
git pull
echo Start building...
cd /root/work/Otium/Otium/Otium && /usr/bin/dotnet publish -c Release -o ../Release
echo Build end.
echo Restarting service...
systemctl restart otiumbuild.service
echo Service restared.
echo Done!