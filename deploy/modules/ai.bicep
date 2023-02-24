param projectname string
param location string
param name string
param workspaceResourceId string

resource insights 'microsoft.insights/components@2020-02-02' = {
  name: name
  location: location
  tags: {
    project: projectname
  }
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: workspaceResourceId
  }
}

output instrumentationKey string = insights.properties.InstrumentationKey
output connectionString string = insights.properties.ConnectionString
