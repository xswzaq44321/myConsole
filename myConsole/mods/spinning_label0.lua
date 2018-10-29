local obj = newMyClass([[lua controlled]], 200, 200)
local r, theta = 100, 0
table.insert( timer.registered,
function()
	theta = theta + (2 * math.pi / 360) * 5
	x = r * math.cos(theta) + 200
	y = r * math.sin(theta) + 200
	obj.setPos(x, y)
end
)