   M S B A M L   `   `   `  ����A  =AppGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AppGUI.WindowsAppGUI.Windows  5   	        AppGUI.Windows.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:AppGUI.Windows     5   	     � Title$  LoginWindow��5   	   $��217.279��6   $	��300��6.    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   .��5      �� $��0,0,0,-1q�6   .+�5   
   �� -   5        $��lbError��5      ���	#FFDA3232$��5,0,0,5q�66   � 5   
    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��25 �6   .+�5   
   �� $��Login:��6   �� 5   
   -   5        $��txtLogin��5      $��1��6"   �� 5   
   $��	Password:��6   $��1��6$   �� 5   
   -   5        $��txtPassword��5      $��1��6%   $��1��62   5      �� 5   
   -   5         ��Name$ BtnLogin��5      $��Login��6#   $��0,0,0,10q�6J   5      5       