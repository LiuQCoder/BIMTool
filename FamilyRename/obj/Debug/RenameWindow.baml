   M S B A M L   `   `   `  ����G  CFamilyRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FamilyRenameFamilyRename  5   	       FamilyRename.RenameWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FamilyRename     5   	   -   5          � Title$  族重命名��5	   	   $	��200��6   $	��350��6#    � 
ResizeMode$ NoResize=�6/    � WindowStartupLocation$ CenterScreen=�6E   .��5
      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   6,   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $
��0.5* �6   6,    � ColumnDefinitions 5   
   �� 5      $
��0.1* �6   6/   �� 5      $��* �6    6,   �� 5      $
��1.5* �6    6/   �� 5      $
��0.2* �6    6/   .+�5   
   �� -   5         ��Name$ newname��5      $��1��6#   $��2��60   $	��150��6@   $��Center=�6M   $��Center=�6j   $��35��6�   $��22(�6�   6�   �� 5   
   $��1��6   $��1��6!   $��30��61   $	��130��6=   $��Center=�6J   $��22(�6f   $��Candara Light)�6v    �� TextAlignment$ Center=�6�   	新名称6�   �� 5   
   -   5         ��Name$ 	btnRename��5   ;   $��2��6   $��2��6!   $	��150��6.   $��Center=�6f   $��Center=�6�   $��22(�6�   $��Candara Light)�6�   .��6�   选择族实例6�   5      5       