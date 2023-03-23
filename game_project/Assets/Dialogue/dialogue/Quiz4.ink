INCLUDE globals.ink
เจอกันอีกเเล้วสินะ ดูสิว่าเธอจะตอบคำถามของฉันได้ไหม #speaker:Norman #portrait:Norman_neutral #layout:right
เอาหละจงฟังให้ดี #speaker:Norman #portrait:Norman_neutral #layout:right
เอาละตั้งใจให้ดีละข้ามีตัวทดลองอยู่6 ตัวอยู่ ได้เเก่ A B C D E F เมื่อวัดความ
สูงของทุกตัวได้ผลเป็นดังต่อไปต่อนี้พบว่า E สูงกว่า C เเต่ เตี้ยกว่า F และ A สูงกว่า B เเต่เตี้ยกว่า C 
เเละ E แต่B ไม่ได้เตี้ยที่สุด คำถามตัวทดลองตัวไหนตัวเตี้ยที่สุด #speaker:Norman #portrait:Norman_neutral #layout:right
-> main

=== main === 
+ [C]
   -> chose1("1")
   
+ [D]
   -> chosewrong("2")
+ [F]
   -> chosewrong("3")
+ [A] 
    -> chosewrong("4")

=== chose1(choice) === 
~NormanQuiz4 = choice
 ถูกต้อง!! ฉลาดไม่เบานี่ #speaker:Norman #portrait:Norman_neutral #layout:right
   ถูกต้องพ่อค้าก็จะขนกล่องใหญ่8 กล่อง ไป 7 รอบ เเละ ขนกล่องเล็ก 10 กล่อง
4 รอบ ก็จะเท่ากับ 11 รอบ ไม่เบานี่เจ้าหนู #speaker:Norman #portrait:Norman_neutral #layout:right

->END
=== chosewrong(choice) ===
~NormanQuiz4 = choice
 เจ้าตอบผิดซะเเล้วละ  #speaker:Norman #portrait:Norman_neutral #layout:right
 ->END
 


