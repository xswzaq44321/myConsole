local obj = newMyClass([[u spin me right round~]], 100, 100)
local r, theta = 50, 0
table.insert( timer.registered,
function()
	theta = theta + (2 * math.pi / 360) * 10
	x = r * math.cos(theta) + 100
	y = r * math.sin(theta) + 100
	obj.setPos(x, y)
end
)