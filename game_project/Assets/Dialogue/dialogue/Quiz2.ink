INCLUDE globals.ink
เจอกันอีกเเล้วสินะ ดูสิว่าเธอจะตอบคำถามของฉันได้ไหม #speaker:Norman #portrait:Norman_neutral #layout:right
เอาหละจงฟังให้ดี #speaker:Norman #portrait:Norman_neutral #layout:right
เอาหละสมมติว่ามีพ่อค้าบรรทุกกล่องใหญ่ 8 กล่อง หรือ กล่องเล็ก 10 กล่องใน
การขนส่งหนึ่งเที่ยวการขนส่งทั้งหมดมีกล่องรวม 96 กล่อง ถ้าในจำนวนนั้นมีกล่องใหญ่มากกว่ากล่องเล็กพ่อค้าต้องขนส่งกี่เที่ยวถึงจะส่งได้น้อยครั้งที่สุด #speaker:Norman #portrait:Norman_neutral #layout:right
-> main

=== main === 
+ [11]
   -> chose1("1")
   
+ [10]
   -> chosewrong("2")
+ [13]
   -> chosewrong("3")


=== chose1(choice) === 
~NormanQuiz2 = choice
 ถูกต้อง!! ฉลาดไม่เบานี่ #speaker:Norman #portrait:Norman_neutral #layout:right
   ถูกต้องพ่อค้าก็จะขนกล่องใหญ่8 กล่อง ไป 7 รอบ เเละ ขนกล่องเล็ก 10 กล่อง
4 รอบ ก็จะเท่ากับ 11 รอบ ไม่เบานี่เจ้าหนู #speaker:Norman #portrait:Norman_neutral #layout:right

->END
=== chosewrong(choice) ===
~NormanQuiz2 = choice
 เจ้าตอบผิดซะเเล้วละ  #speaker:Norman #portrait:Norman_neutral #layout:right
 ->END
 




