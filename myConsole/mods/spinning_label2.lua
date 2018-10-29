local obj = newMyClass([[1]], 150, 150)
local r, theta = 25, 0
table.insert( timer.registered, function()
	theta = theta - (2 * math.pi / 360) * 7
	x = r * math.cos(theta) + 150
	y = r * math.sin(theta) + 150
	obj.setPos(x, y)
end
)