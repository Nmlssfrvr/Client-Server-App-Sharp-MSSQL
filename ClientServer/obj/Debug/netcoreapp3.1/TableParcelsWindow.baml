   M S B A M L   `   `   `  ����G  CClientServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ClientServerClientServer  5   	   %    ClientServer.TableParcelsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ClientServer     5   	     � Title$  TableParcelsWindow��5   	   $	��450��6$   $	��800��61   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ TableParcelsGrid��5
      $��Center=�6-   $	��343��6J   $��0,10,0,0q�6W   $	��Top=�6i   $	��780��6�   $��22(�6�      AutoGenerateColumns$ False��6�      SelectionMode$ Single=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        id��   FontSize$ 14Q�5   1   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6?      
IsReadOnly$
 True��6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *p_id+6d     5      -   5        tax��$ 14Q�5   2   $ 
Налог��6@   $
 True��6i    6O   �� *p_tax+6i     5      -   5        reciever��$ 14Q�5   7   $ Получатель��6E   $
 True��6x    6Y   �� *
p_reciever+6x     5      -   5        	inventory��$ 14Q�5   8   $ 
Опись��6F   $
 True��6u    6U   �� *p_inventory+6u     5      -   5        	index��$ 14Q�5   4   $ Индекс��6B   $
 True��6o    6R   �� *
p_office+6o     5      -   5        
tariff��$ 14Q�5   5   $ 
Тариф��6C   $
 True��6o    6R   �� *
p_tariff+6o   5      �� 5   
   -   5        	 ��Name$	 	AddParcel��5      $��Добавить��6$   $
��Left=�67   $��
10,368,0,0q�6R   $	��Top=�6f   $��40��6~   $	��150��6�   $��14(�6�   6�   �� 5   
   -	   5        $	 
EditParcel��5      $��Изменить��6%   $
��Left=�68   $��220,368,0,0q�6S   $	��Top=�6h   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -
   5        $	 DeleteParcel��5      $��Удалить��6'   $
��Left=�69   $��430,368,0,0q�6T   $	��Top=�6i   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -   5        $	 
UpdateGrid��5      $#��Обновить данные��6%   $
��Left=�6?   $��640,368,0,0q�6Z   $	��Top=�6o   $��40��6�   $	��150��6�   $��14(�6�   6�   5      5      