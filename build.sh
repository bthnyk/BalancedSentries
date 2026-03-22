#!/bin/bash

set -e

rm -rf BalancedSentries/bin BalancedSentries/obj

dotnet build
