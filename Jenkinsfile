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
		DOCKER_IMAGE="${SERVICE_NAME}_${env.BRANCH_NAME}:${env.BUILD_NUMBER}"
		BRANCH_DATABASE_NAME="${SERVICE_NAME}_${env.BRANCH_NAME}"
		DBHOST="localhost"
		KUBEDBHOST="petstore-mysql.default.svc.cluster.local"
		KUBECONFIG="c:\\Users\\hturowski\\.kube\\config"
		NAMESPACE="${env.SERVICE_NAME}-${env.BRANCH_NAME}"
        EXTERNAL_PORT = getExternalPort()
	}

    stages {

       stage('Database Migration') {
            steps {
                echo 'Applying database migrations..'
				dir("PetStore") {
                	bat "dotnet ef database update"
				}
            }
        }

        stage('Unit Tests') {
            steps {
                echo 'Unit testing..'
                bat "dotnet test ./PetStore.Tests"
            }
        }

        stage('Build Docker Container') {
            steps {
                echo 'Building..'
				bat "docker build -t ${env.DOCKER_IMAGE} ."
            }
        }

        stage('Deploy') {
            steps {
			    echo 'Deploying service..'
				dir("Ruby") {
					bat "ruby parse_template.rb > ${env.NAMESPACE}_deployment.yml"
					bat "kubectl --kubeconfig ${env.KUBECONFIG} apply -f ${env.NAMESPACE}_deployment.yml"
				}
            }
        }

        stage('Integration Test') {
			options {
				retry(3)
			}
            steps {
				sleep(10)
                echo 'Integration Testing..'
                bat "dotnet test ./PetStore.Integration.Tests"
            }
        }

       stage('Production Database Migration') {
			when { branch 'master' }
            steps {
                echo 'Applying database migrations..'
				dir("PetStore") {
					bat "set BRANCH_DATABASE_NAME=${env.SERVICE_NAME}"
                	bat "dotnet ef database update"
				}
            }
        }

		stage('Production Deployment') {
			when { branch 'master' }
			steps {
				dir("Ruby") {
					bat "ruby parse_production_template.rb > prod_deployment.yml"
					bat "kubectl --kubeconfig ${env.KUBECONFIG} apply -f prod_deployment.yml"
				}
			}
		}
    }
}
