#!/bin/bash
npm run build --prefix ./Frontend/

scp ./Frontend/dist/cable-wizard/* root@amtmann.de:/root/dockervolumes/service_nginx-config/_data/www/cablewizard/
