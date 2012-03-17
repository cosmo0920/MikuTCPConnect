#include <iostream>
#include <boost/asio.hpp>

int main(int argc, char** argv)
{
  using namespace boost::asio;
  using ip::tcp;
  std::string buf;
  try
    {
	  io_service              service;
	  tcp::resolver           resolver(service);
	  tcp::resolver::query    query(tcp::v4(), "localhost", "3939");
	  tcp::socket             socket(service);
	  tcp::endpoint endpoint(*resolver.resolve(query));
	  
	  std::cout << "connecting to [" << endpoint << "]..." << std::endl;
	  std::cin >> buf;
	  buf += '\n';//RubyのTCPSocketは終端の改行コードでストリームの終を認識してる？
	  socket.connect(endpoint);
	  write(socket, buffer(buf));
    }
  catch (std::exception& e)
    {
	  std::cerr << e.what() << std::endl;
    }
  
  return 0;
}
