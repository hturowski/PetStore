FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /app

COPY . .
RUN dotnet restore

COPY PetStore/. ./PetStore/
WORKDIR /app/PetStore
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/PetStore/out ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
