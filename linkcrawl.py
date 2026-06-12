import socket, time

def connect_to_server(ip, port, retry=10):
    s = socket.socket()
    try:
        s.connect((ip, port))
    except Exception as e:
        print(e)
        if retry > 0:
            time.sleep(1)
            retry -= 1
            connect_to_server(ip, port, retry)
    return s

def get_source(s, ip, page):
    CRLF = '\r\n'
    get = 'GET /' + page + ' HTTP/1.1' + CRLF
    get += 'Host: '
    get += ip
    get += CRLF
    get += CRLF
    s.send(get.encode('utf-8'))
    response = s.recv(10000000).decode('latin-1')
    return response

def get_all_links(response):
    list_links = []
    beg = 0
    while True:
        beg_str = response.find('href="', beg)
        if beg_str == -1:
            return list_links
        end_str = response.find('"', beg_str + 6)
        if end_str == -1:
            return list_links
        link = response[beg_str + 6 : end_str]
        if link not in list_links:
            list_links.append(link)
        beg = end_str + 1
    return list_links

def is_status_ok(response):
    return '200 OK' in response

def is_valid_page(link):
    ignored_extensions = (
        '.css', '.js', '.ico', '.png', '.jpg', '.jpeg',
        '.gif', '.svg', '.woff', '.woff2', '.ttf', '.eot',
        '.pdf', '.xml', '.json', '.zip', '.mp4', '.mp3'
    )
    return not link.endswith(ignored_extensions)

ip = 'www.crawler-test.com'
port = 80

links = ['/']
index = 0
visited = 0 # Brojač posjećenih linkova

while index < len(links) and visited < 50:
    current_link = links[index]
    index += 1

    print("Pokušavam ({index}): {current_link}")

    try:
        s = connect_to_server(ip, port)
        page = current_link.lstrip('/') # uklanjamo početni '/' za slanje zahtjeva
        response = get_source(s, ip, page)
        s.close()
    except Exception as e:
        print("Greška: {e}")
        continue

    if not is_status_ok(response):
        print("Nije 200 OK, preskačem.")
        continue

    visited += 1 # brojimo samo uspješno posjećene linkove
    new_links = get_all_links(response)
    for link in new_links:
        if link.startswith('/') and link not in links and is_valid_page(link):
            links.append(link)

#print("\n--- Svi pronađeni linkovi ---")
#for link in links:
#    print(link)

print("\nUkupno pronađeno linkova: ", len(links))
print("Ukupno posjećenih linkova: ", visited)
