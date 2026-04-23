@description('Globally unique name of the Web App.')
param webAppName string

@description('Azure region for all resources.')
param location string = resourceGroup().location

@description('Name of the App Service Plan.')
param appServicePlanName string = 'asp-${webAppName}'

var linuxFxVersion = 'DOTNETCORE|10.0'

resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: webAppName
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'Disabled'
      minTlsVersion: '1.2'
      http20Enabled: true
      alwaysOn: false
    }
  }
}

output webAppName string = webApp.name
output webAppHostname string = webApp.properties.defaultHostName
