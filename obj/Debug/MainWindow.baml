   M S B A M L   `   `   `  ����	  Laba1    Laba1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   -   5          �Name$  WMain��5   �    � Title$ TI_Laba_1_Vizener��5   	      
MainWindow#���   6#    � SizeToContent$ Height=�6H    � WindowStartupLocation$ CenterScreen=�6_    � Icon$ WebFormTemplate_11274.ico��6�   $	��435��6�   .��5      � .+�5   
   �� $��20,5,0,0q�6   $=��7Исходный текст:                  Язык:��6"   $
��Left=�6S   $	��205��6n   $��30��6z   $	��Top=�6�   �� 5   
   -   5         ��Name$ TbMain��5      $��	20,30,0,0q�6"    �� TextWrapping$
 Wrap=�65   $	��100��6I   $	��Top=�6V   $
��Left=�6n   " �� VerticalScrollBarVisibility$
 Auto=�6�   ���	#FF969696$���~I am little crazy monkey which attack at down
Я маленькая бешеная макака атакующая снизу��6�    �� SelectionBrush �	#FF464646	 �� AcceptsReturn	 . $	��387��6\  
 �� 
CaretBrush

 �   �6h  �� 5	   
   $��
20,135,0,0q�6   $��	Ключ:��6$   $
��Left=�64   $	��310��6O   $��30��6[   $	��Top=�6g   �� 5
   
   -   5        $ TbKey��5
      $��
20,180,0,0q�6!   $
 Wrap=�65   $��20��6I   $	��Top=�6U   $
��Left=�6m   $
 Auto=�6�   ���	#FF969696$��EmaxanЕмахан��6�    �	#FF464646	 . $	��387��6  

 �   �6  �� 5   
   -   5         ��Name$ ExtendedKey��5      $��
20,205,0,0q�6%   $&�� Расширенный ключ:��69   $
��Left=�6U   $	��380��6p   $��30��6|   $	��Top=�6�   �� 5   
   -   5        $ ExtendedKeyText��5      $��
20,235,0,0q�6+   $�� ��6?   $
��Left=�6G   $	��387��6b   $	��100��6n   $	��Top=�6{   ���	#FF969696 �� 
IsReadOnly . $
 Auto=�6�    �� IsReadOnlyCaretVisible . 

 �   �$
 Wrap=�6  �� 5   
   $��
20,335,0,0q�6   $��Шифротекст:��6$   $
��Left=�6:   $	��100��6U   $��30��6a   $	��Top=�6m   �� 5   
   -   5        $ TbCrypto��5      $��
20,365,0,0q�6$   $
 Wrap=�68   $	��100��6L   $	��Top=�6Y   $
��Left=�6q   $
 Auto=�6�   ���	#FF969696 �	#FF464646	 . $	��387��6�   

 �   �6  �� 5   
   -   5         ��Name$ BCoding��5      $��50,480,0,10q�6"   $��30��67   $��Кодировать��6C   ���	#FF969696$
��Left=�6�   $	��127��6�   $	��Top=�6�   �� 5   
   -   5        $ 	BDecoding��5      $��250,480,0,0q�6$   $��30��69   $��Декодировать��6E   ���	#FF969696$
��Left=�6�   $	��127��6�   $	��Top=�6�   �� 5   
   -	   5         ��Name$	 Cb1��5      $��Прямой��6    $
��Left=�61   $��
20,160,0,0q�6L   $	��Top=�6`   ���	#FF9696966�   �� 5   
   -
   5        $	 Cb2��5      $ ��Прогрессивный��6    $
��Left=�68   $��100,160,0,0q�6S   $	��Top=�6h   ���	#FF9696966�   �� 5   
   -   5        $	 Cb3��5      $*��$Самогенерирующийся��6    $
��Left=�6=   $��230,160,20,0q�6X   $	��Top=�6n   ���	#FF9696966�   �� 5   
   -   5         ��Name$ LKasiske��5      $��>��6&   $��Right=�6/   $��0,205,0,232q�6K   $��Center=�6`   $��12��6{   $��23��6�   ���	#FF3C3C3C �� TextAlignment$ Center=�6�   �� 5   
   $��Тест Касиске��6   $��25(�6'   $��430,10,-175,0q�65   $	��160��6L   $��40��6X   $	��Top=�6d   $
��Left=�6|   �� 5   
   -   5        $ Analize��5      $ ��Анализировать��6"   $��25(�6:   $��425,70,-170,0q�6H   $	��180��6_   $��40��6k   $
��Left=�6w   $	��Top=�6�   ���	#FF9696966�   �� 5   
   -   5        $ KasiskeText��5      $��425,130,-170,0q�6'   $�� ��6?   $
��Left=�6G   $	��180��6b   $��40��6n   $	��Top=�6z   ���	#FF969696 . $
 Auto=�6�    . 

 �   �$
 Wrap=�6  �� 5   
   -   5        $ 
KasiskeNew��5      $��425,180,-170,0q�6&   $�� ��6>   $
��Left=�6F   $	��180��6a   $	��290��6m   $	��Top=�6z   ���	#FF969696 . $
 Auto=�6�   $ �� HorizontalScrollBarVisibility$
 Auto=�6�    . 

 �   �$
 Wrap=�64  �� 5   
   -   5        $ 	DopAnaliz��5      $��425,480,-170,0q�6$   $%��Частотный анализ��6<   $
��Left=�6W   $	��Top=�6r   $	��180��6�   $��30��6�   ���	#FF9696966�   �� 5   
   -   5        $
 CbEn��5      $��English��6!   $
��Left=�63   $��
210,10,0,0q�6N   $	��Top=�6b   ���	#FF9696966�   �� 5   
   -   5        $
 CbRu��5      $��Русский��6!   $
��Left=�63   $��
270,10,0,0q�6N   $	��Top=�6b   ���	#FF9696966�   �� 5   
   -   5        $ BOpen��5      $��Открыть...��6    $
��Left=�65   $��
340,10,0,0q�6P   $	��-10q�6d   $	��Top=�6r   $��75��6�   $��15��6�   ���	#FF9696966�   � 5   
   -   5         �Name$	 Bar��5      $��	0,520,0,0q�6#   $��30��66   $��Stretch=�6B   ���	#FF9696966`   �� 5   
   -   5        $ BSave��5      $��Сохранить...��6    $��315,345,0,0q�67   $	��-10q�6L   $��15��6Z   $	��100��6f   $
��Left=�6r   $	��Top=�6�   ���	#FF9696966�   5      5       