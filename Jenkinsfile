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
		DOCKER_IMAGE="${SERVICE_NAME}-${env.BRANCH_NAME}"
		DBNAME="${SERVICE_NAME}_${env.BRANCH_NAME}"
		DBHOST="localhost"
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
				bat "helm upgrade ${env.SERVICE_NAME}-${env.BRANCH_NAME} --install --set production=false,service.port=${env.EXTERNAL_PORT},service.name=${env.SERVICE_NAME}-${env.BRANCH_NAME},replica_count=1,database.name=${env.DBNAME},image.name=${env.DOCKER_IMAGE},image.tag=${env.BUILD_NUMBER} --namespace ${env.SERVICE_NAME}-${env.BRANCH_NAME} ./petstore-chart"
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
				bat "helm upgrade --install --name ${env.SERVICE_NAME} --set production=true,service.port=${env.EXTERNAL_PORT} ./petstore-chart"
			}
		}
    }
}
