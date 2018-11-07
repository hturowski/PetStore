pipeline {
    agent any
	environment{
		SERVICE_NAME="petstore"
		DOCKER_NAME="${SERVICE_NAME}_${env.BRANCH_NAME}:${env.BUILD_NUMBER}"
		DBNAME="${SERVICE_NAME}_${env.BRANCH_NAME}"
		DBHOST="localhost"
		KUBEDBHOST="petstore-mysql.default.svc.cluster.local"
		KUBECONFIG="c:\\Users\\hturowski\\.kube\\config"
		NAMESPACE="${env.SERVICE_NAME}-${env.BRANCH_NAME}"
        SERVICE_PORT="80"
		FEATURE_PORT="81"
		MASTER_PORT="82"
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
					env.SERVICE_PORT=env.FEATURE_PORT
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
				bat "docker build -t ${env.DOCKERNAME} ."
            }
        }

        // stage('Configure Namespace') {
        //     steps {
		// 		bat "kubectl --kubeconfig ${env.KUBECONFIG} create namespace ${env.NAMESPACE}"
		// 	}
        // }

        stage('Deploy') {
            steps {
				bat "kubectl --kubeconfig ${env.KUBECONFIG} set env deployments/${env.SERVICE_NAME} DBNAME=${env.DBNAME} -n ${env.NAMESPACE}"
				bat "kubectl --kubeconfig ${env.KUBECONFIG} set env deployments/${env.SERVICE_NAME} DBHOST=${env.KUBEDBHOST} -n ${env.NAMESPACE}"
				bat "kubectl --kubeconfig ${env.KUBECONFIG} set image deployments/${env.SERVICE_NAME} ${env.SERVICE_NAME}=${env.DOCKERNAME} -n ${env.NAMESPACE}"
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
					bat "kubectl rollout undo deployments/${env.SERVICE_NAME} -n ${env.NAMESPACE}"
				}
			}
        }
    }
}
