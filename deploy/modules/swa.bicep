param name string
param location string
param sku string

resource swa 'Microsoft.Web/staticSites@2021-01-15' = {
  name: name
  location: location
  tags: null
  properties: {}
  sku: {
    name: sku
    size: sku
  }
}

#disable-next-line outputs-should-not-contain-secrets
output deployment_token string = listSecrets(swa.id, swa.apiVersion).properties.apiKey
