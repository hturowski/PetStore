def getServicePort()
{
	if($env.BRANCH_NAME == "master") {
		return "81"
	}
	else {
		return "82"
	}
}

pipeline {
    agent any
	environment{
		SERVICE_NAME="petstore"
		DOCKER_IMAGE="${SERVICE_NAME}_${env.BRANCH_NAME}:${env.BUILD_NUMBER}"
		DBNAME="${SERVICE_NAME}_${env.BRANCH_NAME}"
		DBHOST="localhost"
		KUBEDBHOST="petstore-mysql.default.svc.cluster.local"
		KUBECONFIG="c:\\Users\\hturowski\\.kube\\config"
		NAMESPACE="${env.SERVICE_NAME}-${env.BRANCH_NAME}"
        EXTERNAL_PORT= getServicePort()
		FEATURE_PORT="82"
		MASTER_PORT="81"
	}

    stages {
	   stage('Build Configuration') {
			when {
				expression {
					return env.BRANCH_NAME != "master"
				}
			}
			steps {
				echo 'Configuring build..'
				script {
					EXTERNAL_PORT=FEATURE_PORT
				}
			}
		}

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
				dir("Ruby") {
				echo "External Port: ${env.EXTERNAL_PORT}"
					bat "ruby parse_template.rb > ${env.NAMESPACE}_deployment.yml"
					bat "kubectl --kubeconfig ${env.KUBECONFIG} apply -f ${env.NAMESPACE}_deployment.yml"
				}
				
            }
        }

        stage('Integration Test') {
            steps {
                echo 'Integration Testing..'
                bat "dotnet test ./PetStore.Integration.Tests"
            }
			post {
				failure {
					echo 'Rolling back..'
					bat "kubectl --kubeconfig ${env.KUBECONFIG} rollout undo deployments/${env.SERVICE_NAME} -n ${env.NAMESPACE}"
				}
			}
        }
    }
}
