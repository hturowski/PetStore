def getExternalPort()
{
	def branchName = "${env.BRANCH_NAME}"
	if(branchName == "master") {
		return "81"
	}
	else {
		return "82"
	}
}


pipeline {
    agent any
	environment {
		SERVICE_NAME="petstore"
		DBNAME="${SERVICE_NAME}_${env.BRANCH_NAME}"
		DBHOST="localhost"

		DOCKER_IMAGE="${SERVICE_NAME}-${env.BRANCH_NAME}"
		KUBEDBHOST="petstore-mysql.default.svc.cluster.local"
		KUBECONFIG="c:\\.kube\\config"
		NAMESPACE="${env.BRANCH_NAME}"
        EXTERNAL_PORT = getExternalPort()
	}

    stages {

       stage('Database Migration') {
            steps {
                echo 'Applying database migrations to branch namespace'
				dir("PetStore") {
                	bat "dotnet ef database update"
				}
            }
        }

        stage('Unit Tests') {
            steps {
                echo 'Running unit tests'
                bat "dotnet test ./PetStore.Tests"
            }
        }

        stage('Build Docker Container') {
            steps {
                echo 'Building docker container'
				bat "docker build -t ${env.DOCKER_IMAGE}:${env.BUILD_NUMBER} ."
            }
        }

        stage('Deploy') {
            steps {
			    echo 'Deploying service to branch namespace'
// This will copy secrets from the default namespace to the branch namespace. For simplicity, we're creating the secrets directly from the chart instead but this is not good practice.
//				bat "kubectl create namespace ${env.NAMESPACE} & exit 0"
//				bat "kubectl get secret dbcredentials --namespace default --export -o yaml | kubectl apply --namespace=${env.NAMESPACE} -f - "

				bat "helm upgrade ${env.SERVICE_NAME}-${env.BRANCH_NAME} --install --set production=false,replica_count=1,service.port=${env.EXTERNAL_PORT},service.name=${env.SERVICE_NAME}-${env.BRANCH_NAME},database.name=${env.SERVICE_NAME},branch_name=${env.BRANCH_NAME},image.name=${env.DOCKER_IMAGE},image.tag=${env.BUILD_NUMBER} --namespace ${env.NAMESPACE} ./petstore-chart "
			}
        }
		
        stage('Integration Test') {
			options {
				retry(3)
			}
            steps {
				sleep(10)
                echo 'Running integration tests on branch'
                bat "dotnet test ./PetStore.Integration.Tests"
            }
        }

       stage('Production Database Migration') {
			when { 
				beforeAgent true
				branch 'master'
				}
			environment {
					DBNAME="${env.SERVICE_NAME}"
			}
            steps {
                echo 'Applying database migrations to production'
				dir("PetStore") {
                	bat "dotnet ef database update"
				}
            }
        }

		stage('Production Deployment') {
			when { 
				beforeAgent true
				branch 'master'
			}
			steps {
				echo 'Deploying service to production'
				bat "helm upgrade ${env.SERVICE_NAME} --install --set production=true,replica_count=4,service.port=80,service.name=${env.SERVICE_NAME},database.name=${env.SERVICE_NAME},branch_name=${env.BRANCH_NAME},image.name=${env.DOCKER_IMAGE},image.tag=${env.BUILD_NUMBER} ./petstore-chart "
			}
		}
    }
}
