   M S B A M L   `   `   `  ����G  CClientServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ClientServerClientServer  5   	   )    #ClientServer.TableInventoriesWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ClientServer     5   	     � Title$  TableInventoriesWindow��5   	   $	��450��6(   $	��800��65   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ TableInventoriesGrid��5
      $��Center=�61   $	��343��6N   $��0,10,0,0q�6[   $	��Top=�6m   $	��780��6�   $��22(�6�      AutoGenerateColumns$ False��6�      SelectionMode$ Single=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        id��   FontSize$ 14Q�5   1   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6?      
IsReadOnly$
 True��6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *i_id+6d     5      -   5        	pname��$ 14Q�5   4   $ Имя продукта��6B   $
 True��6{    6X   �� *i_product_name+6{     5      -   5        
amount��$ 14Q�5   5   $ Количество��6C   $
 True��6t    6W   �� *
i_amount+6t     5      -   5        	price��$ 14Q�5   4   $ Цена��6B   $
 True��6l    6P   �� *	i_price+6l     5      -   5        fragile��$ 14Q�5   6   $ Хрупкий товар��6D   $
 True��6y    6[   �� *	i_fragile+6y   5      �� 5   
   -   5        	 ��Name$	 AddInventory��5      $��Добавить��6'   $
��Left=�6:   $��
10,368,0,0q�6U   $	��Top=�6i   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -   5        $	 EditInventory��5      $��Изменить��6(   $
��Left=�6;   $��220,368,0,0q�6V   $	��Top=�6k   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -	   5        $	 DeleteInventory��5      $��Удалить��6*   $
��Left=�6<   $��430,368,0,0q�6W   $	��Top=�6l   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -
   5        $	 
UpdateGrid��5      $#��Обновить данные��6%   $
��Left=�6?   $��640,368,0,0q�6Z   $	��Top=�6o   $��40��6�   $	��150��6�   $��14(�6�   6�   5      5      