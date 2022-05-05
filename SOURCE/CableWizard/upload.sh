#!/bin/bash
npm run build --prefix ./Frontend/

ssh root@amtmann.de "rm -rf /root/dockervolumes/service_nginx-config/_data/www/cablewizard/*"
scp ./Frontend/dist/cable-wizard/* root@amtmann.de:/root/dockervolumes/service_nginx-config/_data/www/cablewizard/
