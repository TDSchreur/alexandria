param projectname string
param location string
param name string

param sku string
param retentionInDays int
param dailyQuotaGb int

resource law 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: name
  location: location
  tags: {
    project: projectname
  }
  properties: {
    sku: {
      name: sku
    }
    retentionInDays: retentionInDays
    workspaceCapping: {
      dailyQuotaGb: dailyQuotaGb
    }
  }
}

output workspaceResourceId string = law.id
