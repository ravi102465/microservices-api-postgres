$param1=$args[0]

if ($param1 -eq 'delete')
{
    kubectl config set-context --current --namespace=postgresql
    kubectl delete -f .\06-migration.yaml
    kubectl delete -f .\05-db-service.yaml
    kubectl delete -f .\04-db-deployment.yaml
    kubectl delete -f .\03-db-configmap.yaml
    kubectl delete -f .\02-local-pc.yaml
    kubectl delete -f .\01-local-pv.yaml
    kubectl delete -f .\07-postgresql-ns.yaml
}
elseif ($param1 -eq 'apply')
{
    kubectl apply -f .\07-postgresql-ns.yaml
    kubectl config set-context --current --namespace=postgresql
    kubectl apply -f .\01-local-pv.yaml
    kubectl apply -f .\02-local-pc.yaml
    kubectl apply -f .\03-db-configmap.yaml
    kubectl apply -f .\04-db-deployment.yaml
    kubectl apply -f .\05-db-service.yaml
    kubectl apply -f .\06-migration.yaml
}
else
{
	Write-Host 'Invalid param'
}
