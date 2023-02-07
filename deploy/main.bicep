param location string = resourceGroup().location
param project_name string
param swa_sku string

var swa_name = '${project_name}-swa'

module sql './modules/swa.bicep' = {
  name: 'swa-deployment'
  params: {
    name: swa_name
    sku: swa_sku
    location: location
  }
}
