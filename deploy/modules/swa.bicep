param projectname string
param location string
param name string
param sku string
param application_insights_instrumentation_key string
param application_insights_connection_string string

resource swa 'Microsoft.Web/staticSites@2021-01-15' = {
  name: name
  location: location
  tags: {
    project: projectname
  }
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    repositoryUrl: 'https://github.com/TDSchreur/alexandria'
    branch: 'main'
    stagingEnvironmentPolicy: 'Enabled'
    allowConfigFileUpdates: true
  }
  sku: {
    name: sku
    size: sku
  }
  resource staticWebAppSettings 'config@2021-01-15' = {
    name: 'appsettings'
    properties: {
      APPINSIGHTS_INSTRUMENTATIONKEY: application_insights_instrumentation_key
      APPLICATIONINSIGHTS_CONNECTION_STRING: application_insights_connection_string
    }
  }
}
output identity string = swa.identity.principalId

#disable-next-line outputs-should-not-contain-secrets
output deployment_token string = listSecrets(swa.id, swa.apiVersion).properties.apiKey
