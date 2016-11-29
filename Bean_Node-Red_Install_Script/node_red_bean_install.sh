#!/bin/bash 
/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
brew install npm
sudo npm install -g --unsafe-perm node-red
npm install noble
mkdir -p ~/.node-red/node_modules
npm install --prefix ~/.node-red node-red-contrib-bean

