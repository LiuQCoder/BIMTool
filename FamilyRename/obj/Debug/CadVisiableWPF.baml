   M S B A M L   `   `   `  ����G  CFamilyRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FamilyRenameFamilyRename  5   	   !    FamilyRename.CadVisiableWPF   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FamilyRename     5   	     � Title$  CAD图层设置��5   	   $	��180��6   $	��300��6&    � WindowStartupLocation$ CenterScreen=�62    � 
ResizeMode$ NoResize=�6W   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.2* �6   6,   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $
��0.2* �6   6,    � ColumnDefinitions 5   
   �� 5      $
��0.1* �6!   60   �� 5      $��1* �6!   6.   �� 5      $��1* �6!   6.   �� 5      $
��0.1* �6!   60   .+�5   
   �� -   5         ��Name$ CADcatPartOFF��5      $��1��6(   $��1��68   $	��110��6E   $��40��6Q   $��16(�6]   .��6�   关闭选定图层6�   �� 5   
   -   5        $ CADcatPartON��5      $��2��6'   $��1��67   $	��110��6D   $��40��6P   $��16(�6\   .��6�   保留选定图层6�   �� 5   
   -   5        $ CADcatAllOn��5      $��1��6&   $��2��66   $	��110��6C   $��40��6O   $��16(�6[   .��6�   图层全开6�   5      5      