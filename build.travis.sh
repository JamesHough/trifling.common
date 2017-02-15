#!/usr/bin/env bash
dotnet restore && dotnet build Trifling.Common/project.json -f netstandard1.6 && dotnet build Trifling.Common.UnitTests/project.json -f netcoreapp1.0
