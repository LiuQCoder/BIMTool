@echo off

rd/s/q /q C:\ProgramData\Autodesk\Revit\Addins\2018\BIMToolLq
rd/s/q /q C:\ProgramData\Autodesk\Revit\Addins\2019\BIMToolLq

del /q C:\ProgramData\Autodesk\Revit\Addins\2018\FamilyRename.addin
del /q C:\ProgramData\Autodesk\Revit\Addins\2019\FamilyRename.addin


echo .
echo 卸载完成
echo .
echo 按任意键退出。。。
pause
