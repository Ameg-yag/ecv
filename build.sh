#!/bin/bash

dotnet build;
clear;
./bin/Debug/netcoreapp3.0/ecv $*
