import sys
import os
import fileinput
import subprocess
 
 
if __name__ == '__main__':
    
    #Jenkins传递 无需手动修改
    jenkinsParams = {
        'WorkSpace':os.environ['WorkSpace'].strip().replace('\\', '/'),
        'ProductName':os.environ['ProductName'].strip()
    } 
 
    #本地参数 需手动修改
    localParams = {
        'UnityPath':'C:\Program Files\Unity\Hub\Editor\2020.1.10f1c1\Editor\Unity.exe',
        'ProjectPath':jenkinsParams['WorkSpace'],
        'LogPath':jenkinsParams['WorkSpace'],
    }
    
    print('Jenkins参数')
    print(jenkinsParams)
    print('本地参数')
    print(localParams)
    
    print('开始构建');
    
    #调用BuildEditor.BuildAPK方法 编译APK    
    cmd = '"' + localParams['UnityPath'] + '" -batchmode -projectPath "' + localParams['ProjectPath'] + '" -nographics -executeMethod CMDBuild.CMDBuildAndroid -logFile "' + localParams['LogPath'] + '" -quit '+ jenkinsParams['ProductName'];
    print(cmd);    
    subprocess.call(cmd);
        
    #在Jenkins中打印Log
    fileHandler = open(localParams['LogPath'], mode='r', encoding='UTF-8')
    report_lines = fileHandler.readlines()
    for line in report_lines:
        print(line.rstrip())
        
    print('结束构建')
