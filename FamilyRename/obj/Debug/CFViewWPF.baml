   M S B A M L   `   `   `  ����G  CFamilyRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FamilyRenameFamilyRename  5   	       FamilyRename.CFViewWPF   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FamilyRename     5   	   -   5          � Title$  VV Pro��5	   	   $	��200��6   $	��400��6%    � WindowStartupLocation$ CenterScreen=�61    � 
ResizeMode$ NoResize=�6V   .��5
      �  � RowDefinitions 5   
   �� 5      $
��0.2* �6   6,   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $
��0.2* �6   6,    � ColumnDefinitions 5   
   �� 5      $
��0.1* �6   6.   �� 5      $��1* �6   6,   �� 5      $��1* �6   6,   �� 5      $
��0.1* �6   6.   .+�5   
   �� -   5         ��Name$ ductVisiable��5      $��1��6'   $��1��67   $	��150��6D   $��35��6P   $��17(�6\   .��6�   风管及相关6�   �� 5   
   -   5        $ pipVisiable��5      $��2��6&   $��1��66   $	��150��6C   $��35��6O   $��17(�6[   .��6�   管道及相关6�   �� 5   
   -   5        $ cableVisiable��5      $��1��6(   $��2��68   $	��150��6E   $��35��6Q   $��17(�6]   .��6�   电缆桥架及配件6�   �� 5   
   -   5        $ insulationVisiable��5      $��2��6-   $��2��6=   $	��150��6J   $��35��6V   $��17(�6b   .��6�   风管与管道隔热层6�   �� 5   
   -   5        $ nethighVisiable��5      $��1��6*   $��3��6:   $	��150��6G   $��35��6S   $��17(�6_   .��6�   净高分析视图6�   �� 5    
   -   5        $ 
grVisiable��5       $��2��6%   $��3��65   $	��150��6B   $��35��6N   $��17(�6Z   .��6�   轴网剖面6�   5"      5#      