#!/usr/bin/ruby

require 'socket'

s = TCPSocket.open("localhost", 3939)
io = gets.to_s
if io == "" then
  s.close
end
s.puts(io)
s.close
