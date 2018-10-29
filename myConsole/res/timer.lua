timer = {}
timer.registered = {}
timer.timeout = function()
	for _,func in pairs(timer.registered) do
		func()
	end
end
