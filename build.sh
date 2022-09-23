#!/bin/bash

systemctl stop otiumbuild.service
cd /root/work/Otium/Otium/Otium && dotnet publish -c Release -o ../Release
systemctl start otiumbuild.service