INCLUDE globals.ink
เจอกันอีกเเล้วสินะ ดูสิว่าเธอจะตอบคำถามของฉันได้ไหม #speaker:Norman #portrait:Norman_neutral #layout:right
เอาหละจงฟังให้ดี #speaker:Norman #portrait:Norman_neutral #layout:right
เอาหละเจ้าหนูฉันมีลำดับตัวเลขมาให้ 7 5 8 4 9 3  เเกคิดว่าตัวเลขต่อไปคืออะไร? #speaker:Norman #portrait:Norman_neutral #layout:right
-> main

=== main === 
+ [10]
   -> chose1("1")
   
+ [2]
   -> chosewrong("2")
+ [11]
   -> chosewrong("3")
+ [1]
   -> chosewrong("4")

=== chose1(choice) === 
~NormanQuiz3 = choice
 ถูกต้อง!! ฉลาดนี่  #speaker:Norman #portrait:Norman_neutral #layout:right
    ถูกต้องช่างสังเกตเสียจริงนะ 7 5 8 4 9 3 ถ้าเจ้าลองมองดูดจะแยกได้เป็น 7 8 
9 และ 5 4 3 ฉะนั้นตัวถัดไปจาก 7 8 9 คือ 10 ยังไงละเจ้าหนู #speaker:Norman #portrait:Norman_neutral #layout:right

->END
=== chosewrong(choice) ===
~NormanQuiz3 = choice
 เจ้าตอบผิดซะเเล้วละ  #speaker:Norman #portrait:Norman_neutral #layout:right
 ->END
 




