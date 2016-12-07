FROM microsoft/aspnetcore

WORKDIR /app

COPY WorkShopCore/src/WorkShopCore/bin/Release/netcoreapp1.1/publish .

ENTRYPOINT ["dotnet", "WorkShopCore.dll"]