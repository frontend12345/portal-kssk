FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY . .
RUN dotnet restore

ENV NODE_VERSION 8.11.3
ENV NODE_FULL_NAME node-v8.11.3-win-x64

#install node and npm as MS no longer does this in any .NET Core nanoserver based images since 2.1
RUN New-Item -ItemType directory -Path /build; \
    Invoke-WebRequest https://nodejs.org/dist/v${env:NODE_VERSION}/${env:NODE_FULL_NAME}.zip -OutFile /build/${env:NODE_FULL_NAME}.zip; \
    Expand-Archive -LiteralPath /build/${env:NODE_FULL_NAME}.zip /build; \
    $newPath = (Get-ItemProperty -Path 'Registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment' -Name PATH).path; \
    $nodeFullName = ${env:NODE_FULL_NAME}; \
    $newPath = $newPath + ';/build/' + $nodeFullName; \
    Set-ItemProperty -Path 'Registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment' -Name PATH -Value $newPath;

RUN dotnet publish -c Release -o out
FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Portal.dll"]