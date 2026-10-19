#!/bin/bash
pkill swaync
swaync &

swaync-client -t -sw