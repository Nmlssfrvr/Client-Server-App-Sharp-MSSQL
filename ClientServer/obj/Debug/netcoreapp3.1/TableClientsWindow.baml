   M S B A M L   `   `   `  ����G  CClientServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ClientServerClientServer  5   	   %    ClientServer.TableClientsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ClientServer     5   	     � Title$  TableClientsWindow��5   	   $	��450��6$   $	��800��61   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ TableClientsGrid��5
      $��Center=�6-   $	��343��6J   $��0,10,0,0q�6W   $	��Top=�6i   $	��779��6�   $��22(�6�      AutoGenerateColumns$ False��6�      SelectionMode$ Single=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        id��   FontSize$ 14Q�5   1   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6?      
IsReadOnly$
 True��6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *c_id+6d     5      -   5        	index��$ 14Q�5   4   $ Индекс��6B   $
 True��6n    6R   �� *	c_index+6n     5      -   5        fullname��$ 14Q�5   7   $ ФИО��6E   $
 True��6k    6R   �� *c_FN+6k     5      -   5        pseries��$ 14Q�5   6   $! Серия паспорта��6D   $
 True��6�    6\   �� *c_passport_series+6�     5      -   5        pnumber��$ 14Q�5   6   $! Номер паспорта��6D   $
 True��6�    6\   �� *c_passport_number+6�   5      �� 5   
   -   5        	 ��Name$	 	AddClient��5      $��Добавить��6$   $
��Left=�67   $��
10,368,0,0q�6R   $	��Top=�6f   $��40��6~   $	��150��6�   $��14(�6�   6�   �� 5   
   -   5        $	 
EditClient��5      $��Изменить��6%   $
��Left=�68   $��220,368,0,0q�6S   $	��Top=�6h   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -	   5        $	 DeleteClient��5      $��Удалить��6'   $
��Left=�69   $��430,368,0,0q�6T   $	��Top=�6i   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -
   5        $	 
UpdateGrid��5      $#��Обновить данные��6%   $
��Left=�6?   $��640,368,0,0q�6Z   $	��Top=�6o   $��40��6�   $	��150��6�   $��14(�6�   6�   5      5      