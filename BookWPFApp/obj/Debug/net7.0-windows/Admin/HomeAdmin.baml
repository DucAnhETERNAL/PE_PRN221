   M S B A M L   `   `   `  ����E  ABookWPFApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BookWPFApp.AdminBookWPFApp.Admin  5   	        BookWPFApp.Admin.HomeAdmin   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:BookWPFApp.Admin     5   	     � Title$  	HomeAdmin��5   	   $	��600��6   $
��1000��6(   .��5	      � .+�5
   
   �� .@�5      �� $��Order Confirmation��6   .��5      � .+�5      �� $��10q�6!   .+�5      �� $��Orders to Confirm��6$   $��16(�6=   $
��Bold"�6K   $��0,0,0,10q�6]   &   System.Windows.Controls.DataGrid  5      -   5          Name$ orderConfirmationDataGrid��5   #      AutoGenerateColumns$
 True��6F   $	��400��6a   $	��950��6n   6z   �� 5      -   5         ��Name$ confirmOrderButton��5   !   $��Confirm Order��6=   $	��150��6U   $��0,10,0,0q�6a   $
��Left=�6s   6�   5      5      5      �� 5      $��Manage Books (CRUD)��6   .��5      �  � ColumnDefinitions 5      �� 5      $	��29* �6+   �� 5      $
��468* �6+   .+�5      �� $��10,10,10,10q�6!   $��2��66   .+�5      �� $��Book ID:��6$   �� 5       -   5        $��bookIdTextBox��5    "   $	��200��67    �� 
IsReadOnly$
 True��6C   �� 5"      $��
Book Name:��6$   $��0,10,0,0q�66   �� 5#      -   5        $��bookNameTextBox��5#   "   $	��200��69   �� 5%      $��Price:��6$   $��0,10,0,0q�62   �� 5&      -   5        $��priceTextBox��5&   "   $	��200��66   �� 5(      $��	Category:��6$   $��0,10,0,0q�65   �� 5)      -   5        $��categoryComboBox��5)   #   $	��200��6;    �� SelectedValuePath$ 
CategoryID��6G    �� DisplayMemberPath$ CategoryName��6f   �� 5,       �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   $��0,10,0,0q�6Y   .+�5-      �� -   5        $��Create��5-   %   $��80��66   6A   �� 5.      -   5        $��Update��5.   %   $��80��66   $��10,0,0,0q�6A   6S   �� 5/      -	   5        $��Delete��5/   %   $��80��66   $��10,0,0,0q�6A   6S   �� 50      -
   5        $��Reset��50   %   $��80��65   $��10,0,0,0q�6@   6R   51        54      -   5        $ booksDataGrid��54   #   $ False��6:   $	��300��6V   $	��950��6c   $��0,20,0,0q�6o   	   
IsReadOnly$
	 True��55   =   
   Columns
 56      0  *System.Windows.Controls.DataGridTextColumn  57   "   ,  &System.Windows.Controls.DataGridColumn   Header$ Book ID��65      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6a      
IsReadOnly$
 True��6m   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *BookID+6m     58   "   $ 	Book Name��65   $	 250 6e    6H   �� *
BookName+6e     59   "   $ Price��65   $	 100 6n    6D   �� *Price+ �� StringFormat$ C��6n     5:   "   $ Category��65   $	 200 6h    6G   �� *CategoryName+6h   5=      5>      5?      5@      �� 5C      $��Manage Categories (CRUD)��6   .��5D      � .+�5E      �� $��10q�6!   .+�5G      �� $��Category ID:��6$   �� 5H      -   5        $��categoryIdTextBox��5H   "   $	��200��6;   $
 True��6G   �� 5J      $��Category Name:��6$   $��0,10,0,0q�6:   �� 5K      -   5        $��categoryNameTextBox��5K   "   $	��200��6=   �� 5N      $ 
Horizontal=�6%   $
��Left=�6>   $��0,10,0,0q�6Y   .+�5O      �� -   5        $��Create��5O   %   $��80��66   6A   �� 5P      -   5        $��Update��5P   %   $��80��66   $��10,0,0,0q�6A   6S   �� 5Q      -   5        $��Delete��5Q   %   $��80��66   $��10,0,0,0q�6A   6S   �� 5R      -   5        $��Reset��5R   %   $��80��65   $��10,0,0,0q�6@   6R   5S        5V      -   5        $ categoriesDataGrid��5V   #   $ False��6?   $	��300��6[   $	��950��6h   $��0,20,0,0q�6t   $
	 True��5W   B   
 5X        5Y   "   $ Category ID��65   $	 150 6i   $
 True��6u    6J   �� *
CategoryID+6u     5Z   "   $ Category Name��65   $	 300 6m    6L   �� *CategoryName+6m   5\      5^      5_      5`      5a      �� 5b   
   -   5        $ logoutButton��5b      $��Logout��6'   $	��100��68   $��30��6D   $��Right=�6P   $	��Top=�6l   $��	0,10,10,0q�6�   5c      5d      5e      