   M S B A M L   `   `   `  ����  RFID_Reader    RFID_Reader.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  RFID Reader��5   	   $	��350��6   $	��525��6*   .��5	      � $��5q�6   $
��Auto��6    � ColumnDefinitions 5
   
   �� 5      $��8* �6   �� 5      $
��111* �6   �� 5      $
��268* �6   �� 5      $	��98* �6   �� 5      $��8* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��auto �6   �� 5      $
��116* �6   �� 5      $
��auto �6   .+�5   
   ]� ��. $��5��6)   $	��Top=�6=   .+�5      o� 5      �� 5   
   -   5        $��textBlockTitle��5   �   
���   �$��28(�6'   $��UltraExpanded&�65   $��Italic$�6Q   $
��Bold"�6d   
�������$��1��6�   $
��Auto��6�   $��Stretch=�6�    �� Padding$ 0q�6�   $��EzBar ��6�    �� TextAlignment$ Center=�6�   $��Center=�6  $��1��6'  $��3��64  �� 5   
   -   5        $��textBoxStatus��5   Z   $��1��6   $��3��6$   $
��Auto��61   $
��Left=�6?   $��Center=�6o   $
��Auto��6�   
���  ��$��5,5,0,5q�6�   
�������$��Not Connected��6�   $��15(�6�   $ 4q�6�   �� 5   
   -   5        $��ClearButton��5   �   
���   �
���   � �� 	ClickMode$ Press=�68   $��Clear��6J   |�.  
�������$��3��6   $
��Auto��6�   $��Stretch=�6�   $��Center=�6�   $��Stretch=�6�   $��Center=�6  $
��Auto��60  $��3��6=  $��5q�6J  $��15(�6p  �� 5   
   -   5        $��checkBoxBeepEnabled��5   �   
�������$
��Beep��6&   $��15(�65   
�������$��2��6V   $��24��6f   $��Right=�6r   $��Bottom=�6�   $��3��6�   $��54��6�   $��5q�6�    d� 	IsChecked 6�   ��    Settings �� Source# [   �� Path$ Default.BeepEnabled�68  5      5       