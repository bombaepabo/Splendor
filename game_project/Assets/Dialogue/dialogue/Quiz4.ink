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
  จะพบว่าตัวทดลอง E สูงกว่าตัวทดลอง C แต่เตี้ยกว่าตัวทดลอง F เรียงเป็น FEC และต่อมาตัวทดลอง A สูงกว่าตัวทดลอง B แต่เตี้ยกว่าตัวทดลอง C และตัวทดลอง E เรียงเป็น FECAB และ ตัวทดลอง B ไม่ได้เตียที่สุด #speaker:Norman #portrait:Norman_neutral #layout:right
  แสดงว่าเหลือเพียงตัวเดี่ยวคือตัวทดลอง D  ที่ไม่ได้บอกว่าเตี้ยหรือสูงกว่าตัวทดลองตัวอื่นๆยังไงละี่เจ้าหนู #speaker:Norman #portrait:Norman_neutral #layout:right

->END
=== chosewrong(choice) ===
~NormanQuiz4 = choice
 เจ้าตอบผิดซะเเล้วละ  #speaker:Norman #portrait:Norman_neutral #layout:right
 ->END
 


