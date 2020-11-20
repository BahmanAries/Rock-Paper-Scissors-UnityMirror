pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh '''
echo $(pwd)

C:\\Program Files\\Unity\\Hub\\Editor\\2019.4.12f1\\Editor\\Unity -batchmode -nographics -projectPath "$(pwd)" -logFile unitylog.log -quit
'''
      }
    }

  }
}