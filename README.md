Тестовое задание.

Есть персонаж, который перемещается на wasd и смотрит всегда на курсор.
По нажатию левой клавиши мыши - обьект игрока стреляет пулей, которая нанесёт урон врагу, а если улетит в другое место - то просто удалится из сцены. 
Противник тоже могут стрелять когда видят в радиусе своей атаки игрока. (Впервые столкнулся с NavMesh, очень круто!)
Когда у противника кончается ХП - он удаляется из сцены, если противников осталось 0, то запускается новая волна в которой будет на 1 противника больше чем в предыдущей. 
Когда у игрока закончится здоровье, оставшиеся противники удалятся и появится текст "GameOver" и кнока перезапуска сцены.
