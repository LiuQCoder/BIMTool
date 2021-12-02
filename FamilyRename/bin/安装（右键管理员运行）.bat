@echo off

md C:\ProgramData\Autodesk\Revit\Addins\2018\BIMToolLq
md C:\ProgramData\Autodesk\Revit\Addins\2019\BIMToolLq


md C:\ProgramData\Autodesk\Revit\Addins\2018\BIMToolLq\icon
md C:\ProgramData\Autodesk\Revit\Addins\2019\BIMToolLq\icon


copy BIMToolLq C:\ProgramData\Autodesk\Revit\Addins\2018\BIMToolLq
copy BIMToolLq C:\ProgramData\Autodesk\Revit\Addins\2019\BIMToolLq
copy BIMToolLq\icon C:\ProgramData\Autodesk\Revit\Addins\2019\BIMToolLq\icon
copy BIMToolLq\icon C:\ProgramData\Autodesk\Revit\Addins\2018\BIMToolLq\icon


del /q C:\ProgramData\Autodesk\Revit\Addins\2018\FamilyRename.addin
del /q C:\ProgramData\Autodesk\Revit\Addins\2019\FamilyRename.addin



copy FamilyRename.addin C:\ProgramData\Autodesk\Revit\Addins\2018
copy FamilyRename.addin C:\ProgramData\Autodesk\Revit\Addins\2019


echo .
echo 安装成功
echo .
echo 按任意键退出。。。
pause
