$param1=$args[0]

if ($param1 -eq 'delete')
{
    kubectl config set-context --current --namespace=postgresql-ns
    kubectl delete -f .\04-postgresdeployment.yaml
    kubectl delete -f .\03-db-configmap.yaml
    kubectl delete -f .\02-pvc.yaml
    kubectl delete -f .\01-namespace.yaml
}
elseif ($param1 -eq 'apply')
{
    kubectl apply -f .\01-namespace.yaml
    kubectl config set-context --current --namespace=postgresql-ns
    kubectl apply -f .\02-pvc.yaml
    kubectl apply -f .\03-db-configmap.yaml
    kubectl apply -f .\04-postgresdeployment.yaml
}
else
{
	Write-Host 'Invalid param'
}
