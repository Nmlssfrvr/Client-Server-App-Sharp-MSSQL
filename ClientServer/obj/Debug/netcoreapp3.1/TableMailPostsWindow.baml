   M S B A M L   `   `   `  ����G  CClientServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ClientServerClientServer  5   	   '    !ClientServer.TableMailPostsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ClientServer     5   	     � Title$  TableMailPostsWindow��5   	   $	��450��6&   $
��1000��63   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ TableMailPostsGrid��5
      $��Center=�6/   $	��343��6L   $��0,10,0,0q�6Y   $	��Top=�6k   $	��978��6�   $��22(�6�      AutoGenerateColumns$ False��6�      SelectionMode$ Single=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        id��   FontSize$ 14Q�5   1   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6?      
IsReadOnly$
 True��6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *m_id+6d     5      -   5        	index��$ 14Q�5   4   $ Индекс��6B   $
 True��6n    6R   �� *	m_index+6n     5      -   5        address��$ 14Q�5   6   $ 
Адрес��6D   $
 True��6q    6S   �� *	m_address+6q     5      -   5        	phone��$ 14Q�5   4   $ Телефон��6B   $
 True��6o    6S   �� *	m_phone+6o     5      -   5        postamat_count��$ 14Q�5   =   $/ )Количество постаматов��6K   $
 True��6�    6j   �� *m_postamat_count+6�     5      -   5        
lastupdate��$ 14Q�5   9   $+ %Последнее изменение��6G   $
 True��6�    6d   �� *m_last_update_time+6�   5      �� 5   
   -   5        	 ��Name$	 AddMailPost��5      $��Добавить��6&   $
��Left=�69   $��
11,368,0,0q�6T   $	��Top=�6h   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -	   5        $	 EditMailPost��5      $��Изменить��6'   $
��Left=�6:   $��280,368,0,0q�6U   $	��Top=�6j   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -
   5        $	 DeleteMailPost��5      $��Удалить��6)   $
��Left=�6;   $��569,368,0,0q�6V   $	��Top=�6k   $��40��6�   $	��150��6�   $��14(�6�   6�   �� 5   
   -   5        $	 
UpdateGrid��5      $#��Обновить данные��6%   $
��Left=�6?   $��839,368,0,0q�6Z   $	��Top=�6o   $��40��6�   $	��150��6�   $��14(�6�   6�   5      5      