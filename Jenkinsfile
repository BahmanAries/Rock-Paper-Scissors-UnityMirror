pipeline {
  agent any
  stages {
    stage('Init') {
      steps {
        echo 'Initializing ...'
      }
    }

    stage('Build') {
      steps {
        sh '''
C:\\Program Files\\Unity -batchmode -nographics -projectPath "$(pwd)" -logFile unitylog.log -quit
'''
      }
    }

  }
}